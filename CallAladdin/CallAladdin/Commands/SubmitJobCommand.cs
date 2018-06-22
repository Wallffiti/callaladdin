using CallAladdin.Helper;
using CallAladdin.Model;
using CallAladdin.ViewModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace CallAladdin.Commands
{
    public class SubmitJobCommand : ICommand
    {
        public JobRequestViewModel jobRequestViewModel { get; private set; }
        public event EventHandler CanExecuteChanged;

        public SubmitJobCommand(JobRequestViewModel viewModel)
        {
            jobRequestViewModel = viewModel;
        }

        public bool CanExecute(object parameter)
        {
            if (parameter == null)
                return false;

            var jobRequest = (Job)parameter;

            if (jobRequest == null || jobRequest.StartDateTime == null || jobRequest.EndDateTime == null)
                return false;

            bool timeIsValid = jobRequest.EndDateTime > jobRequest.StartDateTime;

            if (!timeIsValid)
                return false;

            return !string.IsNullOrEmpty(jobRequest.Title)
            && !string.IsNullOrEmpty(jobRequest.ScopeOfWork)
            && !string.IsNullOrEmpty(jobRequest.City)
            && !string.IsNullOrEmpty(jobRequest.Country)
            && !string.IsNullOrEmpty(jobRequest.Address);
        }

        public void Execute(object parameter)
        {
            jobRequestViewModel.CreateJobRequest();
        }
    }
}
