<template>
    <h1>Employee List</h1>
    <v-table>
        <thead>
            <tr>
                <th>ID</th>
                <th>Name</th>
                <th>Surname</th>
                <th>Patronymic</th>
                <th>Email</th>
                <th>CompanyID</th>
            </tr>
        </thead>
        <tbody>
            <tr v-for="employee in employees" :key="employee.ID">
                <td>{{ employee.ID }}</td>
                <td>{{ employee.Name }}</td>
                <td>{{ employee.Surname }}</td>
                <td>{{ employee.Patronymic }}</td>
                <td>{{ employee.Email }}</td>
                <td>{{ employee.CompanyID }}</td>
            </tr>
        </tbody>
    </v-table>
</template>

<script>
    import apiClient from '@/services/api'
    export default {
        data() {
            return {
                employees: [],
            };
        },
        mounted() {
            this.loadData();
        },
        methods: {
            async loadData() {
                try {
                    const response = await apiClient.get('Employees');
                    this.employees = response.data;
                } catch (e) {
                    console.error('Error loading employees:', e);
                }
            }
        },
    };
</script>

<style scoped>
    /* Add your styling here */
</style>
