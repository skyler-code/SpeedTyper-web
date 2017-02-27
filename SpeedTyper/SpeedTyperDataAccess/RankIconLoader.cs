using System;
using System.Windows;
using System.Windows.Media;

namespace SpeedTyper.DataAccess
{
    public class RankIconLoader
    {

        private string imageResxURI = @"/SpeedTyper;component/Resources/images.xaml";

        private ResourceDictionary _resDictionary;

        public RankIconLoader()
        {
            _resDictionary = new ResourceDictionary();
            _resDictionary.Source =
                new Uri(imageResxURI,
                    UriKind.RelativeOrAbsolute);
        }

        public ImageSource LoadRankIcon(int rankID)
        {
            ImageSource rankIcon;
            string fileName;
            if (rankID < 10)
            {
                fileName = "Rank0" + rankID;
            }
            else
            {
                fileName = "Rank" + rankID;
            }
            try
            {
                rankIcon = (ImageSource)_resDictionary[fileName];
            }
            catch (Exception)
            {

                throw;
            }

            return rankIcon;
        }
    }
}
