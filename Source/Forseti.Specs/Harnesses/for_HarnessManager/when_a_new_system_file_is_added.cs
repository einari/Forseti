using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Machine.Specifications;
using Forseti.Files;
using Forseti.Suites;
using Forseti.Pages;
using Forseti.Harnesses;

namespace Forseti.Specs.Harnesses.for_HarnessManager
{
    public class when_a_new_system_file_is_added : given.a_harness_manager_with_harness_and_fake_file_system_watcher
    {
        static File new_system_file;


        Establish context = () =>
            {
                new_system_file = (File)"Scripts/new_system.js";

            };

        Because of = () => file_system_watcher.TriggerChange(Forseti.Files.FileChange.Added, new_system_file);

        It shouldnt_mark_any_suites_for_execution = () => harness_result_used_to_render_page.ShouldBeNull();

    }

    
}
