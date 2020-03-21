using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using System.Web;

namespace PocketArtCollection.Models
{
    public class PieceOfArt
    {
        [Key]
        public Guid Id { get; set; }
        public string Artist { get; set; }
        public string Title { get; set; }
        public string Period { get; set; }
        public string Techinque { get; set; }
        public string Description { get; set; }
        public int DateOfCreation { get; set; }
        public string Picture { get; set; }
        public string UsersEmail{ get; set; }
    }
}