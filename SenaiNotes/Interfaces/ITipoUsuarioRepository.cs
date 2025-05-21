using SenaiNotes.Dto;
using SenaiNotes.Models;
using SenaiNotes.ViewModel;

namespace SenaiNotes.Interfaces
{
    public interface ITipoUsuarioRepository
    {
        Task<List<ListarTipoUsuarioViewModel>> ListarTipoUsuariosuarioAsync();

        ListarTipoUsuarioViewModel BuscarPorId(int id);

        void Cadastrar(CadastrarTipoUsuarioDto tipoUsuario);

        void Atualizar(CadastrarTipoUsuarioDto tipoUsuario, int id);

        void Deletar (int  id);
    }
}
