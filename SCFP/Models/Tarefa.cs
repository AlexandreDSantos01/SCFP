using SCFP.Models;

public class Tarefa
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public bool Principal { get; set; }

    public ICollection<TarefaDiaria> TarefasDiarias { get; set; } = new List<TarefaDiaria>();
}
