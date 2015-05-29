using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace Auction.Anatation
{
    public class FileSizeAttribute : ValidationAttribute, IClientValidatable
    {

        private readonly int _maxFileSize;
        public FileSizeAttribute(int maxFileSize)
        {
            _maxFileSize = maxFileSize;
        }

        public override bool IsValid(object value)
        {
            var files = value as IEnumerable< HttpPostedFileBase>;
            if (files == null)
            {
                return false;
            }
            long totalSeize = 0;
            foreach (var file in files)
            {
                if(file!=null)
                    totalSeize += file.ContentLength;
            }
            return totalSeize <= _maxFileSize;
        }

        public override string FormatErrorMessage(string name)
        {
            return base.FormatErrorMessage(_maxFileSize.ToString());
        }
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var rule = new ModelClientValidationRule
            {
                ErrorMessage = FormatErrorMessage(_maxFileSize.ToString()),
                ValidationType = "filesize"
            };
            rule.ValidationParameters["maxsize"] = _maxFileSize;
            yield return rule;
        }
    }

}