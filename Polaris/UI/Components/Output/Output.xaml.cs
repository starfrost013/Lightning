using Lightning.Core;
using Polaris.Core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Polaris.UI
{
    /// <summary>
    /// Interaction logic for Output .xaml
    /// </summary>
    public partial class Output : Page
    {
        public List<LoggingMessage> Messages { get; set; }

        public Output()
        {
            Messages = new List<LoggingMessage>(); 
            InitializeComponent();

            Polaris_Output_MessageList.DataContext = Messages;
            UpdateLayout();
        }
        
        public AddMessageResult AddMessage(string Message, MessageSeverity Severity)
        {
            AddMessageResult AMR = new AddMessageResult();

            LoggingMessage MS = new LoggingMessage();

            if (Message == null)
            {
                string ErrorString = "Cannot add a LoggingMessage to Polaris output tab - message string is null!!";

                AMR.FailureReason = ErrorString;
                ErrorManager.ThrowError("Polaris Output Message Manager", "PolarisCannotAddNullMessageToPolarisOutputTab", ErrorString);

                return AMR;
            }


            MS.Message = Message;
            MS.Severity = Severity;

            Messages.Add(MS);

            AMR.Successful = true;
            return AMR;
        }
        
    }
}
