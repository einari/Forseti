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
    public class when_a_description_file_is_deleted_for_an_existing_system_with_2_cases : given.a_harness_manager_with_harness_and_fake_file_system_watcher
    {


        Because of = () => file_system_watcher.TriggerChange(Forseti.Files.FileChange.Deleted, description_for_system);

        It should_not_mark_any_suites_for_execution = () => harness_result.ShouldBeNull();
        It should_remove_the_description_from_its_suite = () => harness.Suites.First(s => s.SystemFile == system).Descriptions
                                                                        .ShouldNotContain(d => d.File == description_for_system);

    }

    
}
