using SenaiNotes.Dto;
using SenaiNotes.Models;
using SenaiNotes.ViewModel;

namespace SenaiNotes.Interfaces
{
    public interface ITagNotasRepository
    {
        List<ListarTagNotasViewModel> ListarTodos();

        ListarTagNotasViewModel BuscarPorId(int id);

        void Cadastrar(TagNotaDto tagNota);

        void Atualizar(int id, TagNotaDto tagNota);

        void Deletar(int id);
    }
}
