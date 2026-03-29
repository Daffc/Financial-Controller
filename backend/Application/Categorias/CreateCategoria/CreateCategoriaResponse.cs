using FinancialControllerServer.Domain.Enums;

namespace FinancialControllerServer.Application.Categorias.CreateCategoria;

public class CreateCategoriaResponse{
    public Guid Id { get; set; }
    public required string Descricao { get; set; }
    public required FinalidadeCategoria Finalidade { get; set; }
}