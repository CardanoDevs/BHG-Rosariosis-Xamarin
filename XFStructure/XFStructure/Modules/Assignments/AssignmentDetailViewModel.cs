using DataStore.Customization.Requests.ClassRoom;
using DataStore.Customization.Responses.ClassRoom;
using DataStore.Customization.Responses.Login;
using DataStore.Customization.Stores;
using Plugin.FilePicker;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamvvm;
using XFStructure.Modules.Shared;
using XFStructure.ViewModels;

namespace XFStructure.Modules.ClassRoom
{
    class AssignmentDetailViewModel : BasePageViewModel
    {
        #region Properties

        private COURS _courseData;
        public COURS CourseData
        {
            get { return _courseData; }
            set { SetField(ref _courseData, value); }
        }

        private USER _userData;
        public USER UserData
        {
            get { return _userData; }
            set { SetField(ref _userData, value); }
        }

        private AssignmentsDetail assignmentDetailResponse;
        public AssignmentsDetail AssignmentsDetailResponse
        {
            get { return assignmentDetailResponse; }
            set
            {
                SetField(ref assignmentDetailResponse, value);
            }
        }

        private Assignment _assignmentDetail;
        public Assignment AssignmentDetail
        {
            get { return _assignmentDetail; }
            set { SetField(ref _assignmentDetail, value); }
        }

        private string _message;
        public string Message
        {
            get { return _message; }
            set { SetField(ref _message, value); }
        }

        private ObservableCollection<AssginmentFile> _assignmentFileList;

        public ObservableCollection<AssginmentFile> AssignmentFileList
        {
            get { return _assignmentFileList; }
            set
            {
                SetField(ref _assignmentFileList, value);
            }
        }

        //private string _filePath;
        //public string FilePath
        //{
        //    get { return _filePath; }
        //    set { SetField(ref _filePath, value); }
        //}

        private bool isVisibleFile;

        public bool IsVisibleFile
        {
            get
            {
                return AssignmentFileList != null && AssignmentFileList.Count > 0;
            }
            set
            {
                SetField(ref isVisibleFile, value);
            }
        }

        public bool ShowPreviousFileSection
        {
            get
            {
                return GetField<bool>();
            }
            set
            {
                SetField(value);
            }
        }

        private bool _showUploadButton;
        public bool ShowUploadButton
        {
            get { return _showUploadButton; }
            set { SetField(ref _showUploadButton, value); }
        }

        private bool _isFirst;
        public bool IsFirst
        {
            get { return _isFirst; }
            set { SetField(ref _isFirst, value); }
        }

        private STUDENT _selectedChild;
        public STUDENT SelectedChild
        {
            get { return _selectedChild; }
            set { _selectedChild = value; }
        }

        private bool _isParent;
        public bool IsParent
        {
            get { return _isParent; }
            set { SetField(ref _isParent, value); }
        }

        #endregion

        #region Command
        private ICommand _navigateToQuizAssignment;
        public ICommand NavigateToQuizAssignment => _navigateToQuizAssignment ?? (_navigateToQuizAssignment = new Command(ExecuteNavigateToQuizAssignment));

        private ICommand _navigateToOpenFile;
        public ICommand NavigateToOpenFile => _navigateToOpenFile ?? (_navigateToOpenFile = new Command(ExecuteNavigateToOpenFile));

        private ICommand _navigateToSaveFile;
        public ICommand NavigateToSaveFile => _navigateToSaveFile ?? (_navigateToSaveFile = new Command(ExecuteNavigateToSaveFile));

        #endregion

        #region Method

        public AssignmentDetailViewModel()
        {
            IsFirst = true;
        }

        public override async void OnAppearing()
        {
            try
            {
                base.OnAppearing();
                if (IsFirst)
                {
                    IsFirst = false;
                    await LoadAssignmentDetails();
                    //ClearFields();
                }
            }
            catch (Exception)
            {

            }
        }

        //private void ClearFields()
        //{
        //    Description = string.Empty;
        //    AssignmentFileList = new ObservableCollection<AssginmentFile>();
        //}

        private async Task LoadAssignmentDetails()
        {
            try
            {
                var result = await PerformAPICall(async () => await new ClassStore().GetAssignmentDetailData(AssignmentDetail.ASSIGNMENT_ID));
                if (result != null)
                {

                    Message = result.Assignment.Message;

                    if (result.Assignment?.Files?.Count > 0)
                    {
                        ShowPreviousFileSection = true;
                    }

                    if (AssignmentDetail.IS_CHECKED && result.Assignment.Message.Trim().Length == 0)
                    {
                        result.Assignment.Message = "No message provided";
                    }

                    AssignmentsDetailResponse = result.Assignment;

                }
            }
            catch (Exception ex)
            {

            }
        }

        private async void ExecuteNavigateToQuizAssignment()
        {
            await this.PopPageAsync<AssignmentDetailViewModel>();
        }

        public async void ExecuteNavigateToOpenFile()
        {
            try
            {
                var requestAccessGrant = await Utils.CheckAndRequestStoragePermission();

                if (requestAccessGrant == PermissionStatus.Granted)
                {
                    //if (AssignmentFileList == null)
                    //{
                    //    AssignmentFileList = new ObservableCollection<AssginmentFile>();
                    //}
                    var filedata = await CrossFilePicker.Current.PickFile();

                    if (filedata != null && !string.IsNullOrEmpty(filedata.FileName))
                    {

                        AssignmentFileList.Add(new AssginmentFile
                        {
                            FileName = filedata.FileName,
                            FileData = filedata.DataArray,
                            FilePath = filedata.FilePath
                        });

                        IsVisibleFile = true;

                        //OnPropertyChanged("AssignmentFileList");

                    }
                }
            }
            catch (Exception)
            {

            }
        }

        private async Task<bool> ValidateAsync()
        {
            try
            {
                if (string.IsNullOrEmpty(Message))
                {
                    if (AssignmentFileList.Count == 0)
                    {
                        await Application.Current.MainPage.DisplayAlert("Error", "Please select file or enter a message.", "OK");
                        return false;
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async void ExecuteNavigateToSaveFile()
        {
            try
            {

                if (!await ValidateAsync()) return;

                AssignmentDetailRequest assignmentDetail = new AssignmentDetailRequest();

                foreach (var item in AssignmentFileList)
                {
                    assignmentDetail.Files.Add(
                        new FileWrapper
                        {
                            FileData = item.FileData,
                            Name = item.FileName,
                            Path = item.FilePath
                        });
                }

                assignmentDetail.Message = Message;

                var result = await PerformAPICall(async () => await new ClassStore().PostAssignmentDetailData(assignmentDetail, AssignmentDetail.ASSIGNMENT_ID, AssignmentDetail.STUDENT_ID));

                if (result != null)
                {
                    await Application.Current.MainPage.DisplayAlert("Success", "Your Assignment is submitted.", "OK");
                    //ClearFields();
                    await this.PopPageAsync();
                }

            }
            catch (Exception ex)
            {
            }
        }
        #endregion
    }

    public class AssginmentFile  :INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            var handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _fileName;

        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value;
                OnPropertyChanged("FileName");
            }
        }

        private byte[] _fileData;

        public byte[] FileData
        {
            get { return _fileData; }
            set { _fileData = value;
                OnPropertyChanged("FileData");
            }
        }

        private string _filePath;

        public string FilePath
        {
            get { return _filePath; }
            set { _filePath = value;
                OnPropertyChanged("FilePath");
            }
        }

        //public string FileName { get; set; }
        //public byte[] FileData { get; set; }
       // public string FilePath { get; set; }
    }
}
