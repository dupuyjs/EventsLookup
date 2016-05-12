using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MeetupLibrary.Helpers
{
    public class UriTemplate
    {
        public string Template { get; private set; }

        private const string parameterPattern = @"\{[a-z, A-Z, _, \[, \]]*\}";
        private const string queryParameterPattern = @"[\?, &][a-z, A-Z, _, \[, \]]*=" + parameterPattern;

        public UriTemplate(string template)
        {
            this.Template = template;
        }

        public Uri BindByName(Uri baseUri, IDictionary<string, string> parameters)
        {
            string pathSegment = this.Template.Split(new char[] { '?' })[0];
            string querySegment = "?" + this.Template.Split(new char[] { '?' })[1];

            //Substitute all variables in Path Segment
            foreach (string variable in parameters.Keys)
            {
                pathSegment = pathSegment.Replace("{" + variable + "}", parameters[variable]);
            }

            //There should be any unsubstituted variable in path segment anymore
            if (Regex.IsMatch(pathSegment, UriTemplate.parameterPattern))
                throw new ArgumentException("One or more path segment parameter values were missing. All path segment parameters must be substituted.");

            //Query Segment
            foreach (string variable in parameters.Keys)
            {
                querySegment = querySegment.Replace("{" + variable + "}", parameters[variable]);
            }

            //remove unsubstituted query parameter "parameter=value" pairs
            foreach (Match match in Regex.Matches(querySegment, UriTemplate.queryParameterPattern))
            {
                querySegment = querySegment.Replace(match.Value, string.Empty);
            }

            //If the first query parameter was missing we ended up removing the '?' separator and have an extraneous '&'
            if (querySegment.StartsWith("&"))
                querySegment = "?" + querySegment.Substring(1);

            return new Uri(baseUri, pathSegment + querySegment);
        }
    }
}
