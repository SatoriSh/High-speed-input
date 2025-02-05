using System.IO;

using System.Windows;

namespace high_speed_input
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            timeToCatchUp.Content = "    До \n1100мс";
            howManyLetters.Content = "4 буквы";
            ViewModel();
            randomWords();
            Timer_Tick();
            checkUserText();
        }

        float tick = 0f;
        bool enterPressed = false;
        float upToValue = 1.100f;
        double record;
        string items4letter = File.ReadAllText("items4letter.txt");
        string items5letter = File.ReadAllText("items5letter.txt");
        string items6letter = File.ReadAllText("items6letter.txt");
        string[] theWords4Letter = new string[1842];
        string[] theWords5Letter = new string[4151];
        string[] theWords6Letter = new string[3576];
        Random rnd = new Random();

        public void ViewModel()
        {
            theWords4Letter = items4letter.Split(' ');
            theWords5Letter = items5letter.Split(' ');
            theWords6Letter = items6letter.Split(' ');
        }

        private void startButton(object sender, RoutedEventArgs e)
        {
            enterPressed = true;
            userText.Text = "";
        }

        async Task randomWords()
        {
            while (!enterPressed)
            {
                await Task.Delay(110);
                switch ((string)howManyLetters.Content)
                {
                    case "4 буквы":
                        rndWord.Text = theWords4Letter[rnd.Next(0, 1843)];
                        break;
                    case "5 букв":
                        rndWord.Text = theWords5Letter[rnd.Next(0, 4152)];
                        break;
                    case "6 букв":
                        rndWord.Text = theWords6Letter[rnd.Next(0, 3577)];
                        break;
                }
            }
        }

        async Task checkUserText()
        {
            while (true)
            {
                await Task.Delay(1);
                if (enterPressed)
                {
                    if (userText.Text == rndWord.Text && tick < upToValue)
                    {
                        enterPressed = false;
                        MessageBox.Show("Вы успели написать слово '" + rndWord.Text + "' менее чем за " + upToValue + "мс");
                        enterPressed = false;
                        userText.Text = "";
                        int sful = Convert.ToInt32(successful.Text);
                        sful++;
                        successful.Text = sful.ToString();
                        if (bestTime.Text == "?сек")
                        {
                            bestTime.Text = Math.Round(tick, 3).ToString();
                        }
                        else
                        {
                            bestTime.Text = bestTime.Text.Replace("сек", "");
                            record = Convert.ToDouble(bestTime.Text);
                            bestTime.Text = bestTime.Text.Insert(5, "сек");
                            bestTime.Text = bestTime.Text.Replace("сек", "");
                            if (tick < record)
                            {
                                bestTime.Text = Math.Round(tick, 3, MidpointRounding.AwayFromZero).ToString();
                            }
                        }
                        tick = 0.0001f;
                        clockTextBlock.Text = "0,000";
                    }
                    if (tick >= upToValue)
                    {
                        enterPressed = false;
                        MessageBox.Show("Вы не успели написать слово '" + rndWord.Text + "' менее чем за " + upToValue +  "мс");
                        enterPressed = false;
                        userText.Text = "";
                        int fal = Convert.ToInt32(fail.Text);
                        fal++;
                        fail.Text = fal.ToString();
                        if (bestTime.Text == "?сек")
                        {
                            bestTime.Text = Math.Round(tick, 3).ToString();
                        }
                        else
                        {
                            bestTime.Text = bestTime.Text.Replace("сек", "");
                            record = Convert.ToDouble(bestTime.Text);
                            bestTime.Text = bestTime.Text.Insert(5,"сек");
                        }
                        if (tick < record)
                        {
                            bestTime.Text = Math.Round(tick, 3).ToString();
                        }
                        tick = 0.0001f;
                        clockTextBlock.Text = "0,000";
                            
                    }
                    randomWords();

                    if (bestTime.Text.Contains("сек"))
                    {
                        //skip
                    }
                    else
                    {
                        bestTime.Text = bestTime.Text.Insert(5, "сек");
                    }
                }
            }
        }

        async Task Timer_Tick()
        {
            while (true)
            {
                await Task.Delay(1);
                if (enterPressed)
                {
                    while (enterPressed)
                    {
                        await Task.Delay(1);
                        clockTextBlock.Text = Math.Round(tick, 3).ToString();
                        tick += 0.001f;
                    }
                }
            }
        }

        private void timeToCatchUpButton(object sender, RoutedEventArgs e)
        {
            if (!enterPressed)
            {
                if ((string)timeToCatchUp.Content == "    До \n1100мс")
                {
                    timeToCatchUp.Content = "    До \n1300мс";
                    upToValue = 1.300f;
                }
                else if ((string)timeToCatchUp.Content == "    До \n1300мс")
                {
                    timeToCatchUp.Content = "    До \n1500мс";
                    upToValue = 1.500f;
                }
                else if ((string)timeToCatchUp.Content == "    До \n1500мс")
                {
                    timeToCatchUp.Content = "    До \n1100мс";
                    upToValue = 1.100f;
                }
            }
        }

        private void howManyLettersButton(object sender, RoutedEventArgs e)
        {
            if (!enterPressed)
            {
                if ((string)howManyLetters.Content == "4 буквы")
                {
                    howManyLetters.Content = "5 букв";
                }
                else if ((string)howManyLetters.Content == "5 букв")
                {
                    howManyLetters.Content = "6 букв";
                }
                else if ((string)howManyLetters.Content == "6 букв")
                {
                    howManyLetters.Content = "4 буквы";
                }
            }
        }
    }
}
