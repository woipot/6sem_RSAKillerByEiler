using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Numerics;
using System.Windows;
using System.Windows.Documents;
using Prism.Commands;
using Prism.Mvvm;

namespace RSAKillerByEiler
{
    public class MainVM : BindableBase
    {
        public ObservableCollection<DataObject> List { get; }

        public class DataObject
        {
            public string E { get; set; }
            public string D { get; set; }
        }

        public string EilerValue { get; set; }

        public DelegateCommand StartCommand { get; }


        public MainVM()
        {
            List = new ObservableCollection<DataObject>();
            StartCommand = new DelegateCommand(Start);
        }

        private void Start()
        {
            var isNum = BigInteger.TryParse(EilerValue, out var eilerValue);

            if (!isNum)
            {
                MessageBox.Show("Not valid Eiler Value");
                return;
            }



            List.Clear();
           
            var eList = GetAllE(eilerValue);
            
            foreach (var e in eList)
            {
                var euclid = new EuclidExtended(e, eilerValue);
                euclid.Solve();

                List.Add(new DataObject() { E = e.ToString(), D = euclid.XN.ToString()});
            }

        }


        private IEnumerable<BigInteger> GetAllE(BigInteger eilerValue)
        {
            var result = new List<BigInteger>();

            for (BigInteger i = 2; i < eilerValue; i++)
            {
                var gcd = Gcd(i, eilerValue);
                if (gcd == 1)
                    result.Add(i);
            }

            return result;
        }

        private BigInteger Gcd(BigInteger first, BigInteger second)
        {
            while (first != 0 && second != 0)
            {
                if (first > second)
                    first %= second;
                else
                    second %= first;
            }

            return first == 0 ? second : first;
        }
    }
}
