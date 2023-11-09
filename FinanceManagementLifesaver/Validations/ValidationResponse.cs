using System.Collections.Generic;

namespace FinanceManagementLifesaver.Validations
{
    public class ValidationResponse
    {
        public static string GetValidatorResponse(bool isValid, IList<FluentValidation.Results.ValidationFailure> result)
        {
            string errorMsg = "";
            if (!isValid)
            {
                for (int i = 0; i < result.Count; i++)
                {
                    errorMsg += result[i] + " \n";
                }
                return errorMsg;
            }
            return errorMsg;
        }
    }
}
