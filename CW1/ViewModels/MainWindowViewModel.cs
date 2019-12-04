using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using CW1.Commands;
using CW1.View;



namespace CW1.ViewModels
{

    /*The MainWindowViewModel is for hold the each properties to the UI
     * The Commands folder on the ViewModel will activate when the Button that has
     * been Bound to it on The UI side has been pressed
     */
    class MainWindowViewModel : BaseViewModel
    {
        public string Type { get; private set; }
        public string Header { get; private set; }
        public string Body { get; private set; }
        public string Sender { get; private set; }
        public string Content { get; private set; }

        public string EmailType { get; private set; }
        public string Incident { get; private set; }
        public string Subject { get; private set; }
        public string Code { get; private set; }
        private string url;
        public string URL
        {
            get
            {
                var urlSet = this.url.Split(';');
                this.url = String.Join("\n", urlSet);
                return url;
            }
            private set
            {
                this.url = value;
            }
        }
        private string hashtags;
        public string Hashtags
        {
            get
            {
                var Set = this.hashtags.Split(';');
                string concat = String.Join("\n", Set);
                return concat;
            }
            private set
            {
                this.hashtags = value;
            }
        }
        private string mentions;
        public string Mentions
        {
            get
            {
                var Set = this.mentions.Split(';');
                string concat = String.Join("\n", Set);
                return concat;
            }
            private set
            {
                this.mentions = value;
            }
        }

        public string SubmitOrderButtonContent { get; private set; }


        /* The SubmitOrderButtonCommand have been set to
         * an new instance of Relaycommand and the relevant method is the ocde which
         * we are wanting the RelayCommand to run for us.
         */
        public ICommand SubmitOrderButtonCommand { get; private set; }

        public System.Windows.Controls.UserControl ContentControlBinding { get; private set; }

        public string MessageHeader { get; set; }
        public string MessageBody { get; set; }

        public MainWindowViewModel()
        {
            SubmitOrderButtonContent = "Submit";

            SubmitOrderButtonCommand = new RelayCommand(ReturnButtonClick);

            ContentControlBinding = new InputView();
        }

        /* The SubmitOrderButtonClick are what will
         * fire when the button on the UI is checked
         */
        private void SubmitOrderButtonClick()
        {
            string message = "{ \"Header\": \"" + MessageHeader + "\" , \"Body\":\"" + MessageBody + "\"}";
            Message hf = new Message(message);
            Dictionary<string, string> msg = hf.StartFilter();
            if (msg["error"] == "1")
            {
                MessageBox.Show("Header Error!");
            }
            else
            {
                try
                {
                    if (msg["Type"] == "SMS")
                    {
                        Type = msg["Type"];
                        Sender = msg["Sender"];
                        Content = msg["Content"];
                        ContentControlBinding = new SMSView();
                    }
                    else if (msg["Type"] == "Email")
                    {
                        if (msg["EmailType"] == "Standard")
                        {
                            Type = msg["Type"];
                            EmailType = msg["EmailType"];
                            Sender = msg["Sender"];
                            URL = msg["URL"];
                            Content = msg["Content"];
                            ContentControlBinding = new EmailView();
                        }
                        else
                        {
                            Type = msg["Type"];
                            EmailType = msg["EmailType"];
                            Sender = msg["Sender"];
                            Subject = msg["Subject"];
                            Code = msg["Code"];
                            URL = msg["URL"];
                            Incident = msg["Incident"];
                            Content = msg["Content"];
                            ContentControlBinding = new SIRView();
                        }
                    }
                    else if (msg["Type"] == "Tweets")
                    {
                        Type = msg["Type"];
                        Sender = msg["Sender"];
                        Hashtags = msg["Hashtags"];
                        Mentions = msg["Mentions"];
                        Content = msg["Content"];
                        ContentControlBinding = new TweetsView();
                    }
                    OnChnaged(nameof(ContentControlBinding));

                    SubmitOrderButtonContent = "Return";
                    OnChnaged(nameof(SubmitOrderButtonContent));

                    SubmitOrderButtonCommand = new RelayCommand(ReturnButtonClick);
                    OnChnaged(nameof(SubmitOrderButtonCommand));
                }
                catch (Exception e)
                {
                    MessageBox.Show("Body error!");
                }
            }
        }


        /* The ReturnButtonClick are what will
         * fire when the button on the UI is checked
         */
        private void ReturnButtonClick()
        {
            SubmitOrderButtonContent = "Submit";
            OnChnaged(nameof(SubmitOrderButtonContent));

            SubmitOrderButtonCommand = new RelayCommand(SubmitOrderButtonClick);
            OnChnaged(nameof(SubmitOrderButtonCommand));

            ContentControlBinding = new InputView();
            OnChnaged(nameof(ContentControlBinding));
        }



    }
}
