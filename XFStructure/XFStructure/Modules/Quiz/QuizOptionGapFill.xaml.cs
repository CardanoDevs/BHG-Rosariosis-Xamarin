using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XFStructure.Views;

namespace XFStructure.Modules.Quiz
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class QuizOptionGapFill : ContentView
    {

        static QuizOptionGapFill instance;

        public static readonly BindableProperty OptionStringProperty = BindableProperty.Create(propertyName: "OptionString",
                                                         returnType: typeof(string),
                                                         declaringType: typeof(QuizOptionGapFill),
                                                         defaultValue: "",
                                                         defaultBindingMode: BindingMode.TwoWay,
                                                         propertyChanged: TitleTextPropertyChanged);

        private static void TitleTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {

            if (!String.IsNullOrEmpty(newValue.ToString()))
            {

                var parts = newValue.ToString().Split('_');

                instance.Area.Children.Add(new Label
                {
                    Text = parts[0],
                    
                });
                instance.Area.Children.Add(new StandardEntry
                {
                    Text = parts[1],
                    BorderColor = Color.Black,
                    BackgroundColor = Color.Yellow,
                });
            }
        }

        public static readonly BindableProperty AnswerProperty = BindableProperty.Create(
            "Answer",
            typeof(string),
            typeof(QuizOptionGapFill)
            );


        public string OptionString { get; set; }

        public string Answer { get; set; }

        public QuizOptionGapFill()
        {
            InitializeComponent();
            instance = this;
        }

    }
}