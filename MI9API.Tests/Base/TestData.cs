using MI9API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MI9TestModule.Base
{
    /// <summary>
    /// Only one instance of the classe to mantain the data consistently
    /// </summary>
    public sealed class TestData
    {
        private static FullPayLoad _FullPayLoad = new FullPayLoad();
        private static FullPayLoad _EmptyFullPayLoad = new FullPayLoad();
        private static FullPayLoad _NullFullPayLoad = new FullPayLoad();
        private static readonly TestData _instance = new TestData();

        private TestData()
        {
            _EmptyFullPayLoad.PayLoad = new List<PayLoad>();
            _NullFullPayLoad.PayLoad = null;
            LoadPayLoad();
        }

        public static TestData Instance
        {
            get { return _instance; }
        }

        public FullPayLoad GetNullPayLoad()
        {
            return _NullFullPayLoad;
        }
        
        public FullPayLoad GetEmptyPayLoad()
        {
            return _EmptyFullPayLoad;
        }
        
        public FullPayLoad PayLoads()
        {
            return _FullPayLoad;
        }

        public void RemovePayLoad(int amount)
        {
            int total = PayLoads().PayLoad.Count;

            for (int i = 1; i <= amount; i++)
            {
                _FullPayLoad.PayLoad.RemoveAt(total-i);
            }
        }

        public void AddPayLoad(bool drm, int episodeCount, bool addImage = true) {

            _FullPayLoad.PayLoad.Add(
                new PayLoad
                {
                    Country = "Any country",
                    Description = "Descripton " + episodeCount,
                    Drm = drm,
                    EpisodeCount = episodeCount,
                    Image = addImage ? new Image
                    {
                        ShowImage = "Image Example successs " + episodeCount
                    } : null,
                    NextEpisode = new NextEpisode
                    {
                    },
                    Seasons = new List<Season>(){
                       new Season {
                        Slug =  "Second try slug"
                       }
                    },
                    Slug = "Primary slug " + episodeCount,
                    Title = "title for test " + episodeCount
                });
        }

        private void LoadPayLoad()
        {
            _FullPayLoad.PayLoad = new List<PayLoad>();

            _FullPayLoad.PayLoad.Add(
                new PayLoad {
                    Country = "Any country",
                    Description = "Descripton",
                    Drm = true,
                    EpisodeCount = 1,
                    Image = new Image {
                        ShowImage = "Image Example successs"
                    },
                    NextEpisode = new NextEpisode {
                    },
                    Seasons = new List<Season>(){
                       new Season {
                        Slug =  "Second try slug"
                       }
                    },
                    Slug = "Primary slug",
                    Title = "title for test"
                });

            _FullPayLoad.PayLoad.Add(
                new PayLoad
                {
                    Country = "Any country 2",
                    Description = "Descripton 2",
                    Drm = true,
                    EpisodeCount = 2,
                    Image = new Image
                    {
                        ShowImage = "Image Example successs 2"
                    },
                    NextEpisode = new NextEpisode
                    {
                    },
                    Seasons = new List<Season>(){
                       new Season {
                        Slug =  "Second try slug 2"
                       }
                    },
                    Slug = "Primary slug 2",
                    Title = "title for test 2"
                }
            );
        }
        
    }
}
