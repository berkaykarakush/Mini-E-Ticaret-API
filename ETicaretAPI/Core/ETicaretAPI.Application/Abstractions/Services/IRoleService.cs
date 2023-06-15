namespace ETicaretAPI.Application.Abstractions.Services
{
    public interface IRoleService
    {
        (object, int) GetRoles(int page, int size);
        Task<(string id, string name)> GetRoleById(string id);
        Task<bool> CreateRole(string name);
        Task<bool> DeleteRole(string id);
        Task<bool> UpdateRole(string id, string name);
    }
}
