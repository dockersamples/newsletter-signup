using SignUp.Web.Logging;
using SignUp.Web.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace SignUp.Web
{
    public partial class SignUp : Page
    {     
        private static Dictionary<string, Country> _Countries;
        private static Dictionary<string, Role> _Roles;

        public static void PreloadStaticDataCache()
        {
            Log.Info("Starting pre-load data cache");
            var stopwatch = Stopwatch.StartNew();

            _Countries = new Dictionary<string, Country>();
            _Roles = new Dictionary<string, Role>();
            using (var context = new SignUpDbEntities())
            {
                _Countries["-"] = context.Countries.First(x => x.CountryCode == "-");
                foreach (var country in context.Countries.Where(x=>x.CountryCode != "-").OrderBy(x => x.CountryName))
                {
                    _Countries[country.CountryCode] = country;
                }

                _Roles["-"] = context.Roles.First(x => x.RoleCode == "-");
                foreach (var role in context.Roles.Where(x => x.RoleCode != "-").OrderBy(x => x.RoleName))
                {
                    _Roles[role.RoleCode] = role;
                }
            }

            Log.Info("Completed pre-load data cache, took: {0}ms", stopwatch.ElapsedMilliseconds);
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                PopulateRoles();
                PopulateCountries();
            }
        }

        private void PopulateRoles()
        {
            ddlRole.Items.Clear();
            ddlRole.Items.AddRange(_Roles.Select(x => new ListItem(x.Value.RoleName, x.Key)).ToArray()); 
        }

        private void PopulateCountries()
        {
            ddlCountry.Items.Clear();
            ddlCountry.Items.AddRange(_Countries.Select(x => new ListItem(x.Value.CountryName, x.Key)).ToArray());
        }

        protected void btnGo_Click(object sender, EventArgs e)
        {
            var country = _Countries[ddlCountry.SelectedValue];
            var role = _Roles[ddlRole.SelectedValue];

            var prospect = new Prospect
            {
                CompanyName = txtCompanyName.Text,
                EmailAddress = txtEmail.Text,
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text
            };

            Log.Info("Saving new prospect, email address: {0}", prospect.EmailAddress);
            var stopwatch = Stopwatch.StartNew();

            using (var context = new SignUpDbEntities())
            {
                //reload child objects:
                prospect.Country = context.Countries.First(x => x.CountryCode == country.CountryCode);
                prospect.Role = context.Roles.First(x => x.RoleCode == role.RoleCode);

                context.AddToProspects(prospect);
                context.SaveChanges();
            }
            Log.Info("Prospect saved, email address: {0}, ID: {1}, took: {2}ms", prospect.EmailAddress, prospect.ProspectId, stopwatch.ElapsedMilliseconds);

            Server.Transfer("ThankYou.aspx");
        }
    }
}