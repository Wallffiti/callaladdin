﻿using CallAladdin.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CallAladdin.ViewModel
{
    public class JobHistoryViewViewModel : BaseViewModel
    {
        private JobViewCommonUserControlViewModel jobviewCommonUserControlViewModel;
        private BaseViewModel parentViewModel;
        private Job selectedJob;

        public JobViewCommonUserControlViewModel JobViewCommonUserControlViewModel
        {
            get
            {
                return jobviewCommonUserControlViewModel;
            }
            set
            {
                jobviewCommonUserControlViewModel = value;
                OnPropertyChanged(nameof(JobViewCommonUserControlViewModel));
            }
        }

        public Job GetSelectedJob()
        {
            return selectedJob;
        }

        public JobHistoryViewViewModel(object owner)
        {
            parentViewModel = (BaseViewModel)owner;

            if (parentViewModel is HistoryUserControlViewModel)
            {
                var historyViewModel = (HistoryUserControlViewModel)parentViewModel;
                this.selectedJob = historyViewModel.GetSelectedJob();
            }

            jobviewCommonUserControlViewModel = new JobViewCommonUserControlViewModel(this);
        }
    }
}
