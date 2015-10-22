﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dat.V1.Utils.Extensions;

namespace Dat.Assets.Scraper.V1.BusinessLogic
{
    /// <summary>
    /// Entity class  that all of the classes which represent a database table inherit.
    /// </summary>
    [Serializable]
    public class Base : IDisposable
    {

        #region >>-- CONSTRUCTORS                                                 -->>--


        /// <summary>
        /// Initializes a new instance of the Entity class.
        /// </summary>
        public Base()
        {
            // BLANK
        }
        public Base(string connectionString)
        {
            this.connectionString = connectionString;
        }

        #endregion

        #region >>-- FIELDS AND PROPERTIES                                        -->>--

        private String connectionString;
        /// <summary>
        /// Gets or sets the connection string.
        /// </summary>
        /// <value>
        /// The connection string.
        /// </value>
        public String ConnectionString
        {
            get { return connectionString; }
            set { connectionString = value; }
        }


        #endregion

        #region >>-- METHODS                                                      -->>--

     
        #endregion

        #region >>-- DISPOSING                                                    -->>--

        /// <summary>
        /// Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.
        /// </summary>
        public void Dispose()
        {

        }

        #endregion                                  -->>--


    }
}
