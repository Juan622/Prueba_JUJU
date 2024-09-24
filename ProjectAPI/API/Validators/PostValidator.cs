using DataAccess.Data;
using System;
using System.Collections.Generic;

namespace API.Validators
{
    public class PostValidator
    {
        public string ValidatorBody(string entityBody)
        {
            if (entityBody.Length > 20)
            {
                if (entityBody.Length > 97)
                {
                    return entityBody.Substring(0, 97) + "...";
                }
                return entityBody;
            }
            throw new Exception("El campo body debe contener mas de 20 Caracteres");
        }

        public string ValidatorType(int entityType, string entityCategory)
        {
            switch (entityType)
            {
                case 1:
                    return entityCategory = "Farándula";
                case 2:
                    return entityCategory = "Política";
                case 3:
                    return entityCategory = "Futbol";
                default:
                    return entityCategory;
            }
        }
    }
}
