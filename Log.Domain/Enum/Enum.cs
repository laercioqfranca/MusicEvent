namespace Log.Domain.Enum
{
    public enum EnumTipoLog : short
    {
        LOGIN = 1,
        ALTERACAO,
        CRIACAO,
        EXCLUSAO
    }

    public enum EnumTipoPerfil : int
    {
        Administrador = 1,
        Cliente = 2
    }

}