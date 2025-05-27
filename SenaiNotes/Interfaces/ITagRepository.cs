using SenaiNotes.Dto;
using SenaiNotes.Models;
using SenaiNotes.ViewModel;

namespace SenaiNotes.Interfaces
{
    public interface ITagRepository
    {
        List<TagViewModel> ListarTodos();

        TagViewModel BuscarPorId(int id);

        void Cadastrar(TagDto tag);

        void Atualizar(int id, TagDto tag);

        void Deletar(int id);

        Tag BuscarPorNomeId(int id, string nome);

    }
}
