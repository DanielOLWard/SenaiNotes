using SenaiNotes.Dto;
using SenaiNotes.Models;

namespace SenaiNotes.Interfaces
{
    public interface ITagRepository
    {
        Tag BuscarPorID(int id);
        void Atualizar(int id, Tag tagNota);
        void Deletar(int id);
        void Cadastrar(TagDto Tag);
        Tag BuscarPorUsuario(int tag);

    }
}
