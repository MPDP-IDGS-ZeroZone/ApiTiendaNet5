using System.Collections.Generic;
using System.Linq;
using ApiTienda.Data;
using ApiTienda.Data.Models;
using ApiTienda.Data.Request;

namespace ApiTienda.Services{
public class CategoriaService
{
    private readonly TiendaBDContext _context;

    public CategoriaService(TiendaBDContext context)
    {
        _context = context;
    }

    public IEnumerable<Categoria> Get(int Id = 0, string Nombre = "")
    {
        IEnumerable<Categoria> Categoria = _context.Categorias.Select(Categoria => Categoria);
        
        if(Id != 0)
        {
            Categoria = Categoria.Where(Categoria => Categoria.Idcategoria == Id);
        }
        
        if(Nombre != "")
        {
            Categoria = Categoria.Where(Categoria => Categoria.Nombre.StartsWith(Nombre)).OrderBy(Categoria => Categoria.Nombre);
        }

        return Categoria;
    }
    
    public Categoria GetById(int Id)
    {
        var Categoria = _context.Categorias.Where(Categoria => Categoria.Idcategoria == Id).First();
        return Categoria;
    }

    public Categoria Create(CategoriaRequest newProducto)
    {
        Categoria Categoria = new Categoria();
        Categoria.Nombre = newProducto.Nombre;
        Categoria.Foto = newProducto.Foto;
        
        _context.Categorias.Add(Categoria);
        _context.SaveChanges();

        return Categoria;
    }

    public void Update(int Id, CategoriaRequest Categoria)
    {
        var existingCategoria = GetById(Id);

        if (existingCategoria is not null)
        {
            existingCategoria.Nombre = Categoria.Nombre;
            existingCategoria.Foto = Categoria.Foto;

            _context.SaveChanges();
        }
    }

    public void Delete(int id)
    {
        var CategoriaToDelete = GetById(id);

        if (CategoriaToDelete is not null)
        {
            _context.Categorias.Remove(CategoriaToDelete);
            _context.SaveChanges();
        }
    }
}
}