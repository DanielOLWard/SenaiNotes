using SenaiNotes.Dto;
using SenaiNotes.Models;

namespace SenaiNotes.Interfaces
{
    public interface ITagNotasRepository
    {
        Tag BuscarPorId(int tag);
        void Cadastrar (TagNotaDto tagN);
        void Deletar (int id );
        void Atualizar(int id, TagNotaDto tagNota);
        void CadastrarTag(TagNotaDto cadastrarTagNotas);
    }
}
