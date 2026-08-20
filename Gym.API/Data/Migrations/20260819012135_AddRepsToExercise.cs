using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gym.API.Migrations
{
    /// <inheritdoc />
    public partial class AddRepsToExercise : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Reps",
                table: "Exercises",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Reps",
                table: "Exercises");
        }
    }
}
