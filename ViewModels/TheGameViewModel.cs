using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TriviaAppClean.Models;
using TriviaAppClean.Services;

namespace TriviaAppClean.ViewModels
{
    public class TheGameViewModel : ViewModelBase
    {
        TriviaWebAPIProxy triviaService;
        public TheGameViewModel(TriviaWebAPIProxy service)
        {
            this.triviaService = service;
            this.NextQuestionCommand = new Command(GetQuestion); // getQuestion כאשר המשחק נפתח בונים כפתור שמעביר כל פעם לשאלה הבאה וקורא לפעולה 
            this.AnswerCommand1 = new Command(GetCorrectAnswer1);
            this.AnswerCommand2 = new Command(GetCorrectAnswer2);
            this.AnswerCommand3 = new Command(GetCorrectAnswer3);
            this.AnswerCommand4 = new Command(GetCorrectAnswer4);
            this.FinishGameCommand = new Command(OnFinishGame);
            // מאתחל את הצבעם של התשובות-בהמשך משנה לפי תשובה נכונה או שגוייה
            this.Answer1Color = new Color();
            this.Answer2Color = new Color();
            this.Answer3Color = new Color();
            this.Answer4Color = new Color();
            Answers = new ObservableCollection<string>();
            NotAnswered = true;
            User u = ((App)App.Current).LoggedInUser;
            // כאשר נכנסים למשחק כל פעם מחדש הניקוד מתעדכן לניקוד החדש
            Score = u.Score;
            GetQuestion();

        }

        private ObservableCollection<string> answers;
        public ObservableCollection<string> Answers
        {
            get { return answers; }
            set
            {
                answers = value;
                OnPropertyChanged();
            }
        }
        public ICommand FinishGameCommand { get; set; }

        private async void OnFinishGame()
            //כאשר רוצים לצאת מהמשחק למסך הבית מעדכנים את הניקוד לכמה שצבר ממשחק זה
        {
            User u = ((App)App.Current).LoggedInUser;
            u.Score = Score;

            await this.triviaService.UpdateUser(u);

            await Shell.Current.Navigation.PopModalAsync();

        }
        public ICommand AnswerCommand1 { get; set; }

        public ICommand AnswerCommand2 { get; set; }

        public ICommand AnswerCommand3 { get; set; }

        public ICommand AnswerCommand4 { get; set; }
        public ICommand NextQuestionCommand { get; set; } // קומנד שעושים לו בינדינג במסך הVIEW של המשחק.

        private Color answer1Color;
        public Color Answer1Color
        { 
            get { return this.answer1Color; }
            set
            {
                this.answer1Color = value;
                OnPropertyChanged();
            }
        }

        private Color answer2Color;
        public Color Answer2Color
        {
            get { return this.answer2Color; }
            set
            {
                this.answer2Color = value;
                OnPropertyChanged();
            }
        }

        private Color answer3Color;
        public Color Answer3Color
        {
            get { return this.answer3Color; }
            set
            {
                this.answer3Color = value;
                OnPropertyChanged();
            }
        }

        private Color answer4Color;
        public Color Answer4Color
        {
            get { return this.answer4Color; }
            set
            {
                this.answer4Color = value;
                OnPropertyChanged();
            }
        }

        private bool notAnswered; // פרופרטי שכל עוד השחקן לא לחץ על שום תשובה, הוא שווה אמת
        // וכפתורי השאלות יפעלו.
        public bool NotAnswered
        {
            get { return this.notAnswered; }
            set
            {
                this.notAnswered = value;
                OnPropertyChanged();
            }
        }

        private AmericanQuestion question;
        public AmericanQuestion Question
        {
            get { return question; }
            set
            {
                question = value;
                OnPropertyChanged();
            }
        }

        private Color buttonColor;
        public Color ButtonColor
        {
            get { return this.buttonColor; }
            set
            {
                this.buttonColor = value;
                OnPropertyChanged();
            }
        }

        private int score;
        public int Score
        // מעדכן את הניקוד הנוכחי בדאטא בייס
        {
            get { return this.score; }
            set
            {
                score = value;
                OnPropertyChanged();
            }
        }

        private async void GetQuestion()
        {
            Question = await triviaService.GetRandomQuestion();
            //ממתין עד שמגיעה דרך השרת באופן רנדומלי
            Answer1Color = Colors.Black;
            Answer2Color = Colors.Black;
            Answer3Color = Colors.Black;
            Answer4Color = Colors.Black;
            ScrambleAnswers();
            NotAnswered = true;

        }
        private void ScrambleAnswers()
            // לאחר הגיעה מהרשת, קוראים לכל התשובות ומערבבים אותן כדי שהתשובה הנכונה לא תהיה תמיד באותו המקום
        {
            this.Answers.Clear();
            this.Answers.Add(Question.CorrectAnswer);
            this.Answers.Add(Question.Bad1);
            this.Answers.Add(Question.Bad2);
            this.Answers.Add(Question.Bad3);
            Shuffle<string>(this.Answers);
            OnPropertyChanged("Answers");
        }

        static void Shuffle<T>(ObservableCollection<T> list)
        {// פעולת עזר שמקבלת אובסרוובל קולקשן ומשנה את כל איבר בה
            Random rng = new Random();
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        private void GetCorrectAnswer1()
            // אם נלחץ על תשובה מספר 1 ניכנס לפעולה הזאת
        {
            string answer = Answers[0];
            if (question != null) //אם קיימת שאלה, ניכנס לבדוק את התשובה
            {
                if (question.CorrectAnswer == answer)
                {
                    Score = Score + 10;  // במידה והתשובה נכונה, הניקוד עולה בהתאם
                    Answer1Color = Colors.Green; // הצבע משתנה בהתאם
                }
                else
                    Answer1Color = Colors.Red; // במידה ושגויה, הצבע מתעדכן לאדום
                NotAnswered = false;
                OnPropertyChanged("Answer1Color");
            }
        }
        private void GetCorrectAnswer2()
        {
            string answer = Answers[1];
            if (question != null)
            {
                if (question.CorrectAnswer == answer)
                {
                    Score = Score + 10;
                    Answer2Color = Colors.Green;
                }
                else
                    Answer2Color = Colors.Red;
                NotAnswered = false;
                OnPropertyChanged("Answer2Color");
            }

        }
        private void GetCorrectAnswer3()
        {
            string answer = Answers[2];
            if (question != null)
            {
                if (question.CorrectAnswer == answer)
                {
                    Score = Score + 10;
                    Answer3Color = Colors.Green;
                }
                else
                    Answer3Color = Colors.Red;
                NotAnswered = false;
                OnPropertyChanged("Answer3Color");
            }
        }
        private void GetCorrectAnswer4()
        {
            string answer = Answers[3];
            if (question != null)
            {
                if (question.CorrectAnswer == answer)
                {
                    Score = Score + 10;
                    Answer4Color = Colors.Green;
                }
                else
                    Answer4Color = Colors.Red;
                NotAnswered = false;
                OnPropertyChanged("Answer4Color");
            }
        }

    }
}
