using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace MOS.Data;
public class MosDbContext : DbContext
{
    public MosDbContext(DbContextOptions<MosDbContext> options): base(options)
    {
    
    }   
    

    
}