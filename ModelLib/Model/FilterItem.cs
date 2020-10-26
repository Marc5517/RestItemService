using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLib.Model
{
    public class FilterItem
    {
        private double _lowQuantity;
        private double _highQuantity;

        public FilterItem()
        {

        }

        public FilterItem(double lowQuantity, double highQuantity)
        {
            _lowQuantity = lowQuantity;
            _highQuantity = highQuantity;
        }

        public double LowQuantity
        {
            get => _lowQuantity;
            set => _lowQuantity = value;
        }

        public double HighQuantity
        {
            get => _highQuantity;
            set => _highQuantity = value;
        }

        public override string ToString()
        {
            return $"{nameof(LowQuantity)}: {LowQuantity}, {nameof(HighQuantity)}: {HighQuantity}";
        }
    }
}
