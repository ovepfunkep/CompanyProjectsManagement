<template>
    <v-col>
        <v-text-field v-model="editedItem.name"
                      label="Name"
                      :rules="[rules.required]"></v-text-field>
    </v-col>
    <v-col>
        <v-text-field v-model="editedItem.dateStarted"
                      label="Date started"></v-text-field>
    </v-col>
    <v-col>
        <v-text-field v-model="editedItem.dateEnded"
                      label="Date ended"></v-text-field>
    </v-col>
    <v-col>
        <v-text-field v-model="editedItem.priority"
                      label="Priority"></v-text-field>
    </v-col>
    <v-col>
        <v-autocomplete v-model="editedItem.managerID"
                        :items="employees"
                        :item-title="getFullName"
                        item-value="id"
                        label="Manager"
                        :rules="[rules.required]">
        </v-autocomplete>
    </v-col>
    <v-col>
        <v-autocomplete v-model="editedItem.customerCompanyID"
                        :items="companies"
                        item-title="name"
                        item-value="id"
                        label="Customer company"
                        :rules="[rules.required]">
        </v-autocomplete>
    </v-col>
    <v-col>
        <v-autocomplete v-model="editedItem.contractorCompanyID"
                        :items="companies"
                        item-title="name"
                        item-value="id"
                        label="Contractor company"
                        :rules="[rules.required]">
        </v-autocomplete>
    </v-col>
</template>

<script>
    import apiClient from "../services/api";

    export default {
        props: {
            editedItem: Object,
        },
        data: () => ({
            companies: [],
            employees: [],
            rules: {
                required: value => !!value || 'Field is required',
            },
        }),
        mounted() {
            this.loadData()
        },
        methods: {
            async loadData() {
                try {
                    const responseCompanies = await apiClient.get('Companies');
                    this.companies = responseCompanies.data;
                    const responseEmployees = await apiClient.get('Employees');
                    this.employees = responseEmployees.data;
                } catch (e) {
                    this.handleError(`Error loading data:${e}`);
                }
            },

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
    }
</script>