<script setup>
    import ProjectEditCardFields from '@/components/ProjectEditCardFields.vue'
</script>

<template>
    <CommonTableComponent :headers="headers" selectedTableName="Projects" :companyEditCardFields="ProjectEditCardFields"></CommonTableComponent>
</template>

<script>
    import CommonTableComponent from '@/components/CommonTableComponent.vue'

    export default {
        data: () => ({
            headers: [
                { title: 'ID', align: 'center', sortable: true, key: 'id', },
                { title: 'Name', align: 'center', sortable: true, key: 'name', },
                { title: 'Date started', align: 'center', sortable: true, key: 'dateStarted', },
                { title: 'Date ended', align: 'center', sortable: true, key: 'dateEnded', },
                { title: 'Priority', align: 'center', sortable: true, key: 'priority', },
                { title: 'Manager', align: 'center', sortable: true, key: 'managerID', value: item => `${item.manager.surname} ${item.manager.name}`},
                { title: 'Customer company ID', align: 'center', sortable: true, key: 'customerCompany.name', },
                { title: 'Contractor company ID', align: 'center', sortable: true, key: 'contractorCompany.name', },
                { title: 'Actions', align: 'center', sortable: false, key: 'actions', },
            ]
        }),
        components: {
            ProjectEditCardFields,
            CommonTableComponent,
        },
        methods: {
            getFullName(employee) {
                if (employee) {
                    var initials = '';
                    if (employee.surname) {
                        initials = `${employee.surname} ${employee.name.charAt(0)}.`;
                        if (employee.patronymic) initials += ` ${employee.patronymic.charAt(0)}.`;
                    } else {
                        initials = `${employee.name}`;
                        if (employee.patronymic) initials += ` ${employee.patronymic}`;
                    }
                    return initials;
                }
                return '';
            },
        },
    };
</script>
