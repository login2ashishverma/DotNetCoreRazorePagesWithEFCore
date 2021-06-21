using Microsoft.EntityFrameworkCore.Migrations;

namespace DotnetCoreRazorWebApp.Services.Migrations
{
    public partial class spGetEmployeeById : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
          string proceedure =  @"Create Procedure spGetEmployeeById
                                    @Id int
                                    as
                                    Begin
                                     Select * from Employees
                                     Where Id = @Id
                                    End";

            migrationBuilder.Sql(proceedure);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string proceedure = @"drop proc spGetEmployeeById";

            migrationBuilder.Sql(proceedure);
        }
    }
}
