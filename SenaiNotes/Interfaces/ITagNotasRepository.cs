using SenaiNotes.Models;

namespace SenaiNotes.Interfaces
{
    public interface ITagNotasRepository
    {
        Tag BuscarPorId(int tagNota);
        void Cadastrar (Tag tagN);
        void Deletar (int id );
        void Atualizar(int id, TagNota tagNota);

    }
}
