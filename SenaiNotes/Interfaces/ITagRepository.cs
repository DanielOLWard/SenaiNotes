using SenaiNotes.Dto;
using SenaiNotes.Models;

namespace SenaiNotes.Interfaces
{
    public interface ITagRepository
    {
        Tag BuscarPorNomeId (int id, string nome);
        void Atualizar(int id, Tag tagNota);
        void Deletar(int id);
        void Cadastrar(TagDto Tag);
        Tag BuscarPorUsuario(int tag);

    }
}
