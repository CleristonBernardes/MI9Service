using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MI9API.Models
{

    public class FullPayLoad
    {
        public List<PayLoad> PayLoad;
        public int Skip;
        public int Take;
        public int TotalRecords;
    }

    public class PayLoad
    {
        public string Country;
        public string Description;
        public bool Drm;
        public int EpisodeCount;
        public string Genre;
        public Image Image;
        public string Language;
        public NextEpisode NextEpisode;
        public string PrimaryColour;
        public List<Season> Seasons;
        public string Slug;
        public string Title;
        public string TvChannel;
    }

    public class Image
    {
        public string ShowImage;
    }

    public class Season
    {
        public string Slug;
    }

    public class NextEpisode
    {
        public string Channel;
        public string ChannelLogo;
        public DateTime? Date;
        public string Html;
        public string Url;
    }


}