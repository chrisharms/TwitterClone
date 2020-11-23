﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TwitterClassLibrary.Comment
{
    public class Comment
    {
        public int Id;
        public int CommentUsername;
        public int CommentPostId;
        public string CommentText;

        public Comment()
        {

        }

        public Comment(int id, int commentUsername, int commentPostId, string commenTText)
        {
            Id = id;
            CommentUsername = commentUsername;
            CommentPostId = commentPostId;
            CommentText = commenTText;
        }

    }
}