using DataStore.Customization.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamvvm;
using XFStructure.BasePages;
using XFStructure.Views;

namespace XFStructure.Modules.Assignments
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Quiz : BasePage, IBasePage<QuizViewModel>
    {

        bool IsFirst = true;

        QuizViewModel vmContext;

        List<Question> questions = new List<Question>();

        int currentQuestion = 0;

        public Quiz()
        {
            
            InitializeComponent();

            //questions.Add(new Question
            //{
            //    Text = "Fill In The Blanks",
            //    Type = "BLANK",
            //    Options = new List<string>
            //    {
            //        "Sky is _ and _ rises in the east and _ sets in the west",
            //        "_ is the cause of Malaria!"
            //    }
            //});
            //questions.Add(new Question
            //{
            //    Text = "Type a long answer for this",
            //    Type = "LONG_ANSWER",
            //    Options = new List<string>
            //    {
            //        "Sky is _ and _ rises in the east and _ sets in the west",
            //        "_ is the cause of Malaria!"
            //    }
            //});

            //questions.Add(new Question
            //{
            //    Text = "Type a short answer for this",
            //    Type = "SHORT_ANSWER",
            //    Options = new List<string>
            //    {
            //        "Sky is _ and _ rises in the east and _ sets in the west",
            //        "_ is the cause of Malaria!"
            //    }
            //});

            //questions.Add(new Question
            //{
            //    Text = "What is a circle in 3 dimensions?",
            //    Type = "CHOOSE_ONE",
            //    Options = new List<string>
            //    {
            //        "Circle",
            //        "Cube",
            //        "Sphere",
            //    }
            //});

            //questions.Add(new Question
            //{
            //    Text = "Which of the following are living things?",
            //    Type = "MCQ",
            //    Options = new List<string>
            //    {
            //        "Pencil",
            //        "Human beings",
            //        "Bottle",
            //        "Plants",
            //    }
            //});

        }        

        protected override void OnAppearing()
        {
            base.OnAppearing();

            if (this.BindingContext != null)
            {
                vmContext = BindingContext as QuizViewModel;
                vmContext.page = this;
            }

            //if (IsFirst)
            //{
            //    IsFirst = false;

                   

            //}

        }

        public void InitQuiz(List<Question> questions)
        {
            this.questions = questions;
            DrawQuestions(currentQuestion);
        }

        void DrawQuestions(int index)
        {

            if (index < 0 || index > questions.Count)
            {
                return;
            }

            var question = questions[index];

            questionText.Text = question.TITLE;

            if (!string.IsNullOrEmpty(question.DESCRIPTION))
            {
                questionDesc.IsVisible = true;
                questionDesc.Text = question.DESCRIPTION;
            }
            else
            {
                questionDesc.IsVisible = false;
            }

            questionNumber.Text = "Question: " + (currentQuestion + 1);
            questionPoints.Text = "Point(s): " + question.POINTS;

            switch (question.TYPE)
            {
                case "text":
                    ClearView();
                    DrawShortQA(question);
                    break;
                case "textarea":
                    ClearView();
                    DrawLongQA(question);
                    break;
                case "select":
                    ClearView();
                    DrawChooseOneQA(question);
                    break;
                case "multiple":
                    ClearView();
                    DrawMCQQA(question);
                    break;
                case "gap":
                    ClearView();
                    DrawBlankQA(question);
                    break;

            }
        }

        void ClearView()
        {
            containerStackLayout.Children.Clear();
        }

        void DrawShortQA(Question question)
        {
            containerStackLayout.Children.Add(new StandardEntry
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Placeholder = "Type your short answer here...",
                Text = (question.Answers.Count > 0) ? question.Answers[0] : string.Empty,
            });
        }

        void DrawLongQA(Question question)
        {
            containerStackLayout.Children.Add(new Editor
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Placeholder = "Type your long answer here...",
                AutoSize = EditorAutoSizeOption.TextChanges,
                Text = (question.Answers.Count > 0) ? question.Answers[0] : string.Empty,
            });
        }

        void DrawChooseOneQA(Question question)
        {

            var i = 0;
            foreach (var item in question.OPTIONS)
            {
                containerStackLayout.Children.Add(new RadioButton
                {
                    HorizontalOptions = LayoutOptions.FillAndExpand,
                    Text = item,
                    GroupName = questionText.Text,
                    IsChecked = (question.Answers.Count > 0) ? (question.Answers[i] == "True" ? true : false) : false,
                });
                i++;
            }
        }

        void DrawMCQQA(Question question)
        {
            var i = 0;
            foreach (var item in question.OPTIONS)
            {

                var stack = new StackLayout
                {
                    Orientation = StackOrientation.Horizontal
                };

                stack.Children.Add(new CheckBox
                {
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    IsChecked = (question.Answers.Count > 0) ? (question.Answers[i] == "True" ? true : false) : false,
                });

                stack.Children.Add(new Label
                {
                    Text = item,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    Margin = new Thickness(-20, 0, 0, 0)
                });

                containerStackLayout.Children.Add(stack);
                i++;
            }

        }

        void DrawBlankQA(Question question)
        {

            var k = 0;

            foreach (var item in question.OPTIONS)
            {

                var parts = item.Split(' ');

                var parentStack = new FlexLayout
                {
                    VerticalOptions = LayoutOptions.StartAndExpand,
                    Wrap = FlexWrap.Wrap,
                };

                var i = 0;

                foreach (var words in parts)
                {

                    if (words.Equals("_"))
                    {
                        parentStack.Children.Add(new StandardEntry
                        {
                            //BackgroundColor = Color.Yellow,
                            TextColor = Color.Black,
                            HeightRequest = 25,
                            FontSize = 14,
                            WidthRequest = 70,
                            CornerRadius = 5,
                            BorderColor = Color.FromHex("#ccc"),
                            BorderThickness = 1,
                            VerticalOptions = LayoutOptions.CenterAndExpand,
                            Margin = new Thickness(0, 5),
                            Text = (question.Answers.Count > 0) ? question.Answers[i + k] : string.Empty,
                        });
                        i++;
                    }
                    else
                    {
                        parentStack.Children.Add(new Label
                        {
                            Text = " " + words + " ",
                            FontSize = 16,
                            //BackgroundColor = Color.FromHex("#eee"),
                            LineBreakMode = LineBreakMode.WordWrap,
                            VerticalOptions = LayoutOptions.CenterAndExpand,
                            Margin = new Thickness(0, 5)
                        });
                    }

                }

                k = i;
                containerStackLayout.Children.Add(parentStack);
            }

        }

        private void Prev_Button_Clicked(object sender, EventArgs e)
        {

            GetAnswerAndSave();

            if ((currentQuestion - 1) < 0)
                return;

            this.currentQuestion--;

            DrawQuestions(currentQuestion);

        }

        private void Next_Button_Clicked(object sender, EventArgs e)
        {

            GetAnswerAndSave();

            if ((currentQuestion + 1) >= this.questions.Count)
                return;

            this.currentQuestion++;

                DrawQuestions(currentQuestion);
            
        }

        void GetAnswerAndSave()
        {
            
            var question = questions[currentQuestion];

            question.Answers = new List<string>();

            var container = containerStackLayout;

            switch (question.TYPE)
            {
                case "text":

                    var standardEntry = (StandardEntry)container.Children.FirstOrDefault();

                    if (string.IsNullOrEmpty(standardEntry.Text))
                    {
                        question.Answers.Add(string.Empty);
                    }
                    else
                    {
                        question.Answers.Add(standardEntry.Text);
                    }

                    break;
                case "textarea":

                    var editor = (Editor)container.Children.FirstOrDefault();

                    if (string.IsNullOrEmpty(editor.Text))
                    {
                        question.Answers.Add(string.Empty);
                    }
                    else
                    {
                        question.Answers.Add(editor.Text);
                    }

                    break;
                case "select":

                    foreach (var item in container.Children)
                    {
                        var radio = (RadioButton)item;

                        question.Answers.Add(radio.IsChecked.ToString());

                    }

                    break;
                case "multiple":

                    foreach (var item in container.Children)
                    {
                        var innerStack = (StackLayout)item;

                        if(innerStack.Children.Count == 2)
                        {
                            var checkbox = (CheckBox) innerStack.Children[0];
                            question.Answers.Add(checkbox.IsChecked.ToString());
                        }

                    }

                    break;
                case "gap":

                    foreach (var item in container.Children)
                    {
                        var innerStack = (FlexLayout)item;

                        foreach (var item2 in innerStack.Children)
                        {
                            
                            if(item2 is StandardEntry)
                            {
                                var standardEntry2 = (StandardEntry)item2;

                                if (string.IsNullOrEmpty(standardEntry2.Text))
                                {
                                    question.Answers.Add(string.Empty);
                                }
                                else
                                {
                                    question.Answers.Add(standardEntry2.Text);
                                }

                            }

                        }

                    }


                    break;

            }
        }

        void GetAnswers()
        {


            //var children = containerStackLayout.Children;

            //foreach (var child in children)
            //{

            //    var layout = (FlexLayout)child;

            //    var answers = "";

            //    foreach (var item in layout.Children)
            //    {

            //        if(item is StandardEntry)
            //        {

            //            var entry = (StandardEntry)item;

            //            answers += entry.Text + " ";
            //        }

            //    }

            //    await DisplayAlert("Answers: ", answers, "Ok");

            //}
        }

        private void Submit_Clicked(object sender, EventArgs e)
        {
            this.SubmitQuiz();
        }

        public void SubmitQuiz()
        {
            GetAnswerAndSave();

            vmContext.SubmitQuiz(questions);
        }

    }

}