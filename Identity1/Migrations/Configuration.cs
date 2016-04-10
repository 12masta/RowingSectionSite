using Identity1.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Data.Entity.Validation;
using System.Text;


namespace Identity1.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<Identity1.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            this.AddUserAndRoles();

            //var players = new List<Player>
            //{
            //    new Player{ BirthDate = DateTime.Parse(""), JoinDate = DateTime.Parse(""), User = }
            //};

            var users = context.Users.ToList();

            var birthDate = new List<DateTime>();
            birthDate.Add(DateTime.Parse("1992-08-21"));
            birthDate.Add(DateTime.Parse("1990-06-23"));
            birthDate.Add(DateTime.Parse("1991-02-12"));
            birthDate.Add(DateTime.Parse("1992-12-01"));
            birthDate.Add(DateTime.Parse("1993-11-02"));
            birthDate.Add(DateTime.Parse("1990-02-01"));

            var joinDate = new List<DateTime>();
            joinDate.Add(DateTime.Parse("2014-01-01"));
            joinDate.Add(DateTime.Parse("2013-02-10"));
            joinDate.Add(DateTime.Parse("2012-03-20"));
            joinDate.Add(DateTime.Parse("2011-04-25"));
            joinDate.Add(DateTime.Parse("2014-05-23"));
            joinDate.Add(DateTime.Parse("2013-06-03"));

            var players = new List<Player>();
            //dodawanie zawodnikow
            for (int i = 0; i < 5; i++)
            {
                players.Add(new Player { BirthDate = birthDate.First(), JoinDate = joinDate.First(), User = users.First() });

                birthDate.RemoveAt(0);
                joinDate.RemoveAt(0);
                users.RemoveAt(0);
            }
            players.ForEach(s => context.Players.AddOrUpdate(s));
            SaveChanges(context);


            var trainings = new List<Training>
            {
                new Training{ Name = "2x30", TrainingDate = DateTime.Parse("2014-12-15")},
                new Training{ Name = "3x15", TrainingDate = DateTime.Parse("2014-12-16")},
                new Training{ Name = "40 + si³ownia", TrainingDate = DateTime.Parse("2014-12-17")},
                new Training{ Name = "1/20/1", TrainingDate = DateTime.Parse("2014-12-18")},
                new Training{ Name = "12 km", TrainingDate = DateTime.Parse("2014-12-19")},
                new Training{ Name = "2 x 6 km", TrainingDate = DateTime.Parse("2014-12-08")},
                new Training{ Name = "1/20/2", TrainingDate = DateTime.Parse("2014-12-09")},
                new Training{ Name = "30 min+si³ownia", TrainingDate = DateTime.Parse("2014-12-10")},
                new Training{ Name = "3x12(3,3,3,3)co 10 watt mocniej", TrainingDate = DateTime.Parse("2014-12-11")},
                new Training{ Name = "60 min", TrainingDate = DateTime.Parse("2014-12-12")}
            };
            trainings.ForEach(s => context.Trainings.AddOrUpdate(s));
            SaveChanges(context);

            var enrollments = new List<Enrollment>
            {
                new Enrollment {
                    PlayerID = 1,
                    TrainingID = 1,
                    TrainingTime = TimeSpan.Parse("1:00:00"), NumbersOfMeters = 12000, Rate = 21, EnrollemntDate = DateTime.Parse("2014-12-16")
                },
                new Enrollment {
                    PlayerID = 1,
                    TrainingID = 2,
                    TrainingTime = TimeSpan.Parse("0:45:00"), NumbersOfMeters = 10000, Rate = 21, EnrollemntDate = DateTime.Parse("2014-12-17")
                },
                new Enrollment {
                    PlayerID = 1,
                    TrainingID = 3,
                    TrainingTime = TimeSpan.Parse("0:40:00"), NumbersOfMeters = 8000, Rate = 22, EnrollemntDate = DateTime.Parse("2014-12-18")
                },
                new Enrollment {
                    PlayerID = 1,
                    TrainingID = 4,
                    TrainingTime = TimeSpan.Parse("0:50:00"), NumbersOfMeters = 9000, Rate = 23, EnrollemntDate = DateTime.Parse("2014-12-19")
                },
                new Enrollment {
                    PlayerID = 1,
                    TrainingID = 5,
                    TrainingTime = TimeSpan.Parse("1:00:00"), NumbersOfMeters = 12000, Rate = 20, EnrollemntDate = DateTime.Parse("2014-12-20")
                },
                new Enrollment {
                    PlayerID = 2,
                    TrainingID = 1,
                    TrainingTime = TimeSpan.Parse("1:10:00"), NumbersOfMeters = 12500, Rate = 21, EnrollemntDate = DateTime.Parse("2014-12-21")
                },
                new Enrollment {
                    PlayerID = 2,
                    TrainingID = 2,
                    TrainingTime = TimeSpan.Parse("0:30:00"), NumbersOfMeters = 10600, Rate = 22, EnrollemntDate = DateTime.Parse("2014-12-22")
                },
                new Enrollment {
                    PlayerID = 2,
                    TrainingID = 3,
                    TrainingTime = TimeSpan.Parse("0:52:00"), NumbersOfMeters = 8423, Rate = 23, EnrollemntDate = DateTime.Parse("2014-12-23")
                },
                new Enrollment {
                    PlayerID = 2,
                    TrainingID = 4,
                    TrainingTime = TimeSpan.Parse("0:42:00"), NumbersOfMeters = 8700, Rate = 21, EnrollemntDate = DateTime.Parse("2014-12-24")
                },
                new Enrollment {
                    PlayerID = 2,
                    TrainingID = 5,
                    TrainingTime = TimeSpan.Parse("1:05:00"), NumbersOfMeters = 12000, Rate = 22, EnrollemntDate = DateTime.Parse("2014-12-25")
                },
                new Enrollment {
                    PlayerID = 3,
                    TrainingID = 6,
                    TrainingTime = TimeSpan.Parse("1:00:00"), NumbersOfMeters = 11500, Rate = 23, EnrollemntDate = DateTime.Parse("2014-12-16")
                },
                new Enrollment {
                    PlayerID = 3,
                    TrainingID = 7,
                    TrainingTime = TimeSpan.Parse("0:44:00"), NumbersOfMeters = 9500, Rate = 21, EnrollemntDate = DateTime.Parse("2014-12-17")
                },
                new Enrollment {
                    PlayerID = 3,
                    TrainingID = 8,
                    TrainingTime = TimeSpan.Parse("0:39:00"), NumbersOfMeters = 7500, Rate = 24, EnrollemntDate = DateTime.Parse("2014-12-18")
                },
                new Enrollment {
                    PlayerID = 3,
                    TrainingID = 9,
                    TrainingTime = TimeSpan.Parse("0:49:00"), NumbersOfMeters = 8500, Rate = 21, EnrollemntDate = DateTime.Parse("2014-12-19")
                },
                new Enrollment {
                    PlayerID = 3,
                    TrainingID = 1,
                    TrainingTime = TimeSpan.Parse("0:58:00"), NumbersOfMeters = 11456, Rate = 22, EnrollemntDate = DateTime.Parse("2014-12-20")
                },
                new Enrollment {
                    PlayerID = 4,
                    TrainingID = 1,
                    TrainingTime = TimeSpan.Parse("1:06:00"), NumbersOfMeters = 12000, Rate = 23, EnrollemntDate = DateTime.Parse("2014-12-21")
                },
                new Enrollment {
                    PlayerID = 4,
                    TrainingID = 2,
                    TrainingTime = TimeSpan.Parse("0:42:00"), NumbersOfMeters = 9000, Rate = 21, EnrollemntDate = DateTime.Parse("2014-12-22")
                },
                new Enrollment {
                    PlayerID = 4,
                    TrainingID = 3,
                    TrainingTime = TimeSpan.Parse("0:55:00"), NumbersOfMeters = 8600, Rate = 23, EnrollemntDate = DateTime.Parse("2014-12-23")
                },
                new Enrollment {
                    PlayerID = 4,
                    TrainingID = 4,
                    TrainingTime = TimeSpan.Parse("0:34:00"), NumbersOfMeters = 9345, Rate = 21, EnrollemntDate = DateTime.Parse("2014-12-24")
                },
                new Enrollment {
                    PlayerID = 4,
                    TrainingID = 5,
                    TrainingTime = TimeSpan.Parse("0:55:00"), NumbersOfMeters = 12000, Rate = 23, EnrollemntDate = DateTime.Parse("2014-12-25")
                }

            };
            foreach (Enrollment e in enrollments)
            {
                var enrollmentInDatabase = context.Enrollments.Where(
                    s =>
                    s.Player.ID == e.PlayerID &&
                    s.Training.ID == e.TrainingID).SingleOrDefault();
                if (enrollmentInDatabase == null)
                {
                    context.Enrollments.Add(e);
                }
            }
            SaveChanges(context);


            var contests = new List<Contest>
            {
                new Contest { Name = "I rzut ligii", Description = "Dd.", ContestDate = DateTime.Parse("2014-10-12")},
                new Contest { Name = "II rzut ligii", Description = "Obecnosæ-START-obowi¹zkowa,sk³adki i certyfikat te osoby co jeszcze tego nie zrobi³y...do koñca przysz³ego tygodnia!!!!!!!!!!", ContestDate = DateTime.Parse("2015-01-26")},
                new Contest { Name = "III rzut ligii", Description = "ssda", ContestDate = DateTime.Parse("2015-02-15")},
                new Contest { Name = "Eleminacje do mistrzostw Polski", Description = "dsasda", ContestDate = DateTime.Parse("2015-04-24")},
                new Contest { Name = "Mistrzostwa Polski", Description = "dddddddd", ContestDate = DateTime.Parse("2015-05-05")}
            };
            contests.ForEach(s => context.Contests.AddOrUpdate(s));
            //context.SaveChanges();
            SaveChanges(context);

            var contestEnr = new List<ContestEnrollment>
            {
                new ContestEnrollment { PlayerID = 1, ContestID = 1, EnrollmentDate = DateTime.Parse("2014-10-11")},
                new ContestEnrollment { PlayerID = 1, ContestID = 2, EnrollmentDate = DateTime.Parse("2015-01-25")},
                new ContestEnrollment { PlayerID = 1, ContestID = 3, EnrollmentDate = DateTime.Parse("2015-02-14")},
                new ContestEnrollment { PlayerID = 1, ContestID = 4, EnrollmentDate = DateTime.Parse("2015-04-23")},
                new ContestEnrollment { PlayerID = 1, ContestID = 5, EnrollmentDate = DateTime.Parse("2015-05-04")},
                new ContestEnrollment { PlayerID = 2, ContestID = 1, EnrollmentDate = DateTime.Parse("2014-10-11")},
                new ContestEnrollment { PlayerID = 2, ContestID = 2, EnrollmentDate = DateTime.Parse("2015-01-25")},
                new ContestEnrollment { PlayerID = 2, ContestID = 3, EnrollmentDate = DateTime.Parse("2015-02-14")},
                new ContestEnrollment { PlayerID = 2, ContestID = 4, EnrollmentDate = DateTime.Parse("2015-04-23")},
                new ContestEnrollment { PlayerID = 3, ContestID = 5, EnrollmentDate = DateTime.Parse("2015-05-04")},
                new ContestEnrollment { PlayerID = 3, ContestID = 1, EnrollmentDate = DateTime.Parse("2014-10-11")},
                new ContestEnrollment { PlayerID = 3, ContestID = 2, EnrollmentDate = DateTime.Parse("2015-01-25")},
                new ContestEnrollment { PlayerID = 4, ContestID = 3, EnrollmentDate = DateTime.Parse("2015-02-15")},
                new ContestEnrollment { PlayerID = 4, ContestID = 4, EnrollmentDate = DateTime.Parse("2015-04-23")},
                new ContestEnrollment { PlayerID = 4, ContestID = 5, EnrollmentDate = DateTime.Parse("2015-05-04")},
                new ContestEnrollment { PlayerID = 4, ContestID = 1, EnrollmentDate = DateTime.Parse("2014-10-11")}
            };
            foreach (ContestEnrollment e in contestEnr)
            {
                var contestEnrInDatabase = context.ContestEnrollments.Where(
                    s =>
                    s.Player.ID == e.PlayerID &&
                    s.Contest.ID == e.ContestID).SingleOrDefault();
                if (contestEnrInDatabase == null)
                {
                    context.ContestEnrollments.Add(e);
                }
            }
            SaveChanges(context);


        }

        bool AddUserAndRoles()
        {
            bool success = false;
            var context = new ApplicationDbContext();

            var idManager = new IdentityManager();
            success = idManager.CreateRole("Admin");
            if (!success == true) return success;

            success = idManager.CreateRole("CanEdit");
            if (!success == true) return success;

            success = idManager.CreateRole("User");
            if (!success) return success;


            var newUser = new ApplicationUser()
            {
                UserName = "geralt0001",
                FirstName = "Marcin",
                LastName = "Stanek",
                Email = "stanek.marcinp@gmail.com"
            };

            success = idManager.CreateUser(newUser, "Koalicja1");
            if (!success) return success;

            success = idManager.AddUserToRole(newUser.Id, "Admin");
            if (!success) return success;

            success = idManager.AddUserToRole(newUser.Id, "CanEdit");
            if (!success) return success;

            success = idManager.AddUserToRole(newUser.Id, "User");
            if (!success) return success;

            ///////////////////////////////////////////////////////////////////////

            var newUser1 = new ApplicationUser()
            {
                UserName = "Admin",
                FirstName = "Admin",
                LastName = "Admin",
                Email = "example@example.com"
            };

            success = idManager.CreateUser(newUser1, "Password1");
            if (!success) return success;

            success = idManager.AddUserToRole(newUser1.Id, "Admin");
            if (!success) return success;

            success = idManager.AddUserToRole(newUser1.Id, "CanEdit");
            if (!success) return success;

            success = idManager.AddUserToRole(newUser1.Id, "User");
            if (!success) return success;

            ///////////////////////////////////////////////////////////////////////

            var newUser2 = new ApplicationUser()
            {
                UserName = "wioslarz1",
                FirstName = "Wojtek",
                LastName = "Kempisty",
                Email = "example1@example.com"
            };

            success = idManager.CreateUser(newUser2, "Password1");
            if (!success) return success;


            success = idManager.AddUserToRole(newUser2.Id, "CanEdit");
            if (!success) return success;

            success = idManager.AddUserToRole(newUser2.Id, "User");
            if (!success) return success;

            ///////////////////////////////////////////////////////////////////////

            var newUser3 = new ApplicationUser()
            {
                UserName = "wioslarz2",
                FirstName = "Kamil",
                LastName = "Grzywa",
                Email = "example2@example.com"
            };

            success = idManager.CreateUser(newUser3, "Password1");
            if (!success) return success;

            success = idManager.AddUserToRole(newUser3.Id, "CanEdit");
            if (!success) return success;

            success = idManager.AddUserToRole(newUser3.Id, "User");
            if (!success) return success;

            ///////////////////////////////////////////////////////////////////////

            var newUser4 = new ApplicationUser()
            {
                UserName = "wioslarz3",
                FirstName = "Michal",
                LastName = "Sztaba",
                Email = "example3@example.com"
            };

            success = idManager.CreateUser(newUser4, "Password1");
            if (!success) return success;

            success = idManager.AddUserToRole(newUser4.Id, "User");
            if (!success) return success;

            ///////////////////////////////////////////////////////////////////////

            var newUser5 = new ApplicationUser()
            {
                UserName = "wioslarz4",
                FirstName = "Justyna",
                LastName = "Jakubas",
                Email = "example4@example.com"

            };

            success = idManager.CreateUser(newUser5, "Password1");
            if (!success) return success;

            success = idManager.AddUserToRole(newUser5.Id, "User");
            if (!success) return success;

            return success;
        }

        private void SaveChanges(DbContext context)
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                );
            }
        }
    }
}
