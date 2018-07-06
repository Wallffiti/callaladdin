using System;
using System.Collections.Generic;
using System.Text;

namespace CallAladdin.ViewModel
{
    public class ContractorProfileViewModel : BaseViewModel
    {
        BaseViewModel parentViewModel;

        public ContractorProfileViewModel(object owner)
        {
            if (owner is JobHistoryViewViewModel)
            {
                var jobHistoryViewViewModel = (JobHistoryViewViewModel)owner;
                parentViewModel = jobHistoryViewViewModel;
                var contractorProfile = jobHistoryViewViewModel.GetContractorProfile();
            }
        }
    }
}
