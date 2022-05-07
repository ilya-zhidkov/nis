using AutoMapper;
using Caliburn.Micro;
using Nis.Application.DTOs;
using Nis.Core.Persistence;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Nis.WpfApp.ViewModels
{
    public class TestComboViewModel : Screen
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public TestComboViewModel(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
        }

        IWindowManager manager = new WindowManager();

        public void btn_check()
        {
            manager.ShowWindow(new TestCheckViewModel(), null, null);
        }

    }
}
