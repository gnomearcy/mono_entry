using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Repository.Common
{
    public enum RepositoryStatusCodes
    {
        /// <summary>
        ///  Denotes that a repository method didn't produce an exception but the intention wasn't fulfilled.
        ///  For example, this code should be returned when the insert method doesn't insert anything because the object already exists
        ///  on the target container (in case of an insert, this is usually a database)
        /// </summary>
        NO_OP = 0,

        /// <summary>
        /// Denotes that a repository method produced a valid result with no errors
        /// </summary>
        SUCCESS = 1,

        /// <summary>
        /// Denotes that an exception occurred in the execution of repository method.
        /// </summary>
        ERROR = 2
    }
}
