using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Voting.DAL.Entities;

namespace Voting.BAL.Builders
{
    public class ModelBuilder : IModelBuilder
    {
        private Model _model;
        public ModelBuilder()
        {
            _model = new Model();
        }
        public Model Build()
        {
            return _model;
        }

        public IModelBuilder WithImages(IEnumerable<string> images)
        {
            foreach (var url in images)
            {
                var image = new Image { Url = url };
                _model.Images.Add(image);
            }
            return this;
        }

        public IModelBuilder WithName(string name)
        {
            _model.Name = name;
            return this;
        }
    }
}
