using FinancialControllerServer.Domain.Entities;

namespace FinancialControllerServer.Domain.Interfaces;

public interface ITransacaoRepository
{
    Task Create(Transacao transacao);
    Task Save();
}