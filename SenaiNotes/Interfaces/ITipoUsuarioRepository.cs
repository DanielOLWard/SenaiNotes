using SenaiNotes.Models;
using SenaiNotes.ViewModel;

namespace SenaiNotes.Interfaces
{
    public interface ITipoUsuarioRepository
    {
        Task<List<TipoUsuario>> ListarTipoUsuariosuarioAsync();

        TipoUsuario BuscarPorId(int id);

        void Cadastrar(TipoUsuario tipoUsuario);

        void Atualizar(TipoUsuario tipoUsuario, int id);

        TipoUsuario Deletar (int  id);
    }
}
