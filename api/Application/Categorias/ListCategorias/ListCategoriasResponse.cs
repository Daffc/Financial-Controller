using FinancialControllerServer.Domain.Enums;

namespace FinancialControllerServer.Application.Categorias.ListCategorias;

public class ListCategoriasResponse
{
    public Guid Id { get; set; }
    public required string Descricao { get; set; }
    public required FinalidadeCategoria Finalidade { get; set; }
}