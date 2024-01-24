<script setup>
    import EmployeeEditCardFields from '@/components/EmployeeEditCardFields.vue'
</script>

<template>
    <CommonTableComponent :headers="headers" selectedTableName="Employees" :companyEditCardFields="EmployeeEditCardFields"></CommonTableComponent>
</template>

<script>
    import CommonTableComponent from '@/components/CommonTableComponent.vue'

    export default {
        data: () => ({
            headers: [
                { title: 'ID', align: 'center', sortable: true, key: 'id', },
                { title: 'Name', align: 'center', sortable: true, key: 'name', },
                { title: 'Surname', align: 'center', sortable: true, key: 'surname', },
                { title: 'Patronymic', align: 'center', sortable: true, key: 'patronymic', },
                { title: 'Email', align: 'center', sortable: true, key: 'email', },
                { title: 'Company', align: 'center', sortable: true, key: 'company.name', },
                { title: 'Actions', align: 'center', sortable: false, key: 'actions', },
            ]
        }),
        components: {
            EmployeeEditCardFields,
            CommonTableComponent,
        }
    };
</script>
