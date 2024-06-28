﻿using Projekt.Entities;
using Projekt.Models.Contract;

namespace Projekt.Repositories
{
    public interface IAgreementRepository
    {
        Task<int> AddProductAgreement(ProductAgreementAddRequest request, int supportDuration, CancellationToken cancellationToken);
        Task<SoftwareContract> GetProductAgreementById(int agreementId, CancellationToken cancellationToken);
        Task<List<SoftwareContract>> GetProductAgreements(int productId, DateTime startDate, DateTime endDate, CancellationToken cancellationToken);
    }
}