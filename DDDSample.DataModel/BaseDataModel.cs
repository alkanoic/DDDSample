using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDSample.DataModel
{
    public abstract class BaseDataModel
    {

        private DateTime _createOn;
        public DateTime CreateOn
        {
            get
            {
                if(_createOn != new DateTime())
                {
                    return _createOn;
                }
                _createOn = DateTime.Now;
                return _createOn;
            }
            set
            {
                _createOn = value;
            }
        }

        private DateTime _updateOn;
        public DateTime UpdateOn
        {
            get
            {
                if (_updateOn != new DateTime())
                {
                    return _updateOn;
                }
                _updateOn = DateTime.Now;
                return _updateOn;
            }
            set
            {
                _createOn = value;
            }
        }

        public string UpdateUserId
        {
            get
            {
                return CommonDataModelSetting.CurrentUserId;
            }
        }

    }
}
