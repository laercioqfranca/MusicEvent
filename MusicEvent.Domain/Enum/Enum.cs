namespace MusicEvent.Domain.Enum
{
    public enum EnumTipoLog : short
    {
        LOGIN = 1,
        UPDATE,
        CREATE,
        DELETE
    }

    public enum EnumTipoPerfil : int
    {
        Administrador = 1,
        Cliente = 2
    }

}