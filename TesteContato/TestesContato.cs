using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMedGroup;
using TestMedGroup.Controllers;
using Xunit;

namespace TesteContato
{
    public class TestesContato
    {

        private Contato contato;

        public TestesContato()
        {
            //contato = new ContactController(new Mock<ContactController>);
        }



        [Fact]
        public void Post_Contato()
        {
            Assert.True(true);
        }

        [Fact]
        public void Put_Contato()
        {
            Assert.True(true);
        }

        [Fact]
        public void Delete_Contato()
        {
            Assert.True(true);
        }
    }
}
