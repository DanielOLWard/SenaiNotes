using Microsoft.AspNetCore.Routing.Constraints;
using SenaiNotes.Dto;
using SenaiNotes.Models;
using SenaiNotes.ViewModel;

namespace SenaiNotes.Interfaces
{
    public interface INotaRepository
    {
            List<ListarNotaViewModel> ListarTodos();
            Nota BuscarPorId(int id);
        CadastrarNotaDto Cadastrar(CadastrarNotaDto nota);
            void Atualizar(int id, CadastrarNotaDto nota);
            void Deletar(int id);
            void Arquivar(int id);
        }
    }

