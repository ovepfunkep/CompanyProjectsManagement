<script setup>
    import CompanyEditCardFields from '@/components/CompanyEditCardFields.vue'
</script>

<template>
    <CommonTableComponent :headers="headers" selectedTableName="Companies" :companyEditCardFields="CompanyEditCardFields"></CommonTableComponent>
</template>

<script>
    import CommonTableComponent from '@/components/CommonTableComponent.vue'

    export default {
        data: () => ({
            headers: [
                { title: 'ID', align: 'center', sortable: true, key: 'id', },
                { title: 'Name', align: 'center', sortable: true, key: 'name', },
                { title: 'Actions', align: 'center', sortable: false, key: 'actions', },
            ]
        }),
        components: {
            CompanyEditCardFields,
            CommonTableComponent,
        }
    };
</script>
