<template>
    <section style="justify-content: space-between; display:flex">
        <v-btn v-if="selectedTableName != 'Companies'"
               @click="switchTable('Companies')"
               style="margin:10px"
               color="primary">
            Companies
        </v-btn>
        <v-btn v-if="selectedTableName != 'Employees'"
               @click="switchTable('Employees')"
               style="margin:10px"
               color="primary">
            Employees
        </v-btn>
        <v-btn v-if="selectedTableName != 'Projects'"
               @click="switchTable('Projects')"
               style="margin:10px"
               color="primary">
            Projects
        </v-btn>
    </section>
    <v-data-table v-model="selected"
                  :headers="headers"
                  :items="entries"
                  style="border-radius: 10px;"
                  show-select>
        <template v-slot:top>
            <v-toolbar flat
                       style="border-radius: 10px">
                <v-toolbar-title style="font-weight: bold; 
                                        font-size: 25px; 
                                        color: #2f72ea; 
                                        margin-left:24px">
                    {{ selectedTableName }}
                </v-toolbar-title>
                <v-btn prepend-icon="mdi-delete" 
                       v-if="selected.length != 0" 
                       variant="outlined" 
                       @click="deleteSelected" 
                       color="red">
                    Delete selected items
                </v-btn>
                <v-dialog v-model="dialog"
                          max-width="500px">
                    <template v-slot:activator="{ props }">
                        <v-icon color="primary"
                                size="x-large"
                                v-bind="props"
                                style="font-size: 3em; margin-right: 24px">
                            mdi-plus-box
                        </v-icon>
                    </template>
                    <v-card>
                        <v-card-title>
                            <span class="text-h5">{{ formTitle }}</span>
                        </v-card-title>

                        <component :is="companyEditCardFields" :editedItem="editedItem"></component>

                        <v-card-actions>
                            <v-spacer></v-spacer>
                            <v-btn color="blue-darken-1"
                                   variant="text"
                                   @click="close">
                                Cancel
                            </v-btn>
                            <v-btn color="blue-darken-1"
                                   variant="text"
                                   @click="save">
                                Save
                            </v-btn>
                        </v-card-actions>
                    </v-card>
                </v-dialog>
                <v-dialog v-model="dialogDelete" max-width="500px">
                    <v-card prepend-icon="mdi-alert-circle-outline" title="Confirm" style="border-top: 3px solid red" class="mx-auto" >
                        <v-card-text class="mx-auto">
                            Items with required fields having these items will be deleted as well! Are you sure you want to delete these items?
                        </v-card-text>
                        <v-card-actions>
                            <v-spacer></v-spacer>
                            <v-btn color="blue-darken-1" variant="text" @click="closeDelete">Cancel</v-btn>
                            <v-btn color="blue-darken-1" variant="text" @click="deleteSelectedConfirm">OK</v-btn>
                            <v-spacer></v-spacer>
                        </v-card-actions>
                    </v-card>
                </v-dialog>
            </v-toolbar>
        </template>
        <template v-slot:item.actions="{ item }">
            <v-icon size="small"
                    class="me-2"
                    @click="editItem(item)">
                mdi-pencil
            </v-icon>
        </template>
        <template v-slot:no-data>
            <v-progress-circular v-if="isDataLoading" indeterminate style="margin: 3%"></v-progress-circular>
            <v-btn v-if="!isDataLoading"
                   color="primary"
                   @click="loadData"
                   style="margin: 3%">
                Reset
            </v-btn>
        </template>
    </v-data-table>
    <section style="justify-content:end">
        <v-alert v-if="successMessage" type="success" icon="mdi-check-decagram-outline" style="margin:3%">
            {{ successMessage }}
        </v-alert>
        <v-alert v-if="errorMessage" type="error" icon="mdi-alert-circle-outline" style="margin:3%">
            {{ errorMessage}}
        </v-alert>
    </section>
</template>

<script>
    import apiClient from '@/services/api.js'

    export default {
        props: {
            selectedTableName: String,
            companyEditCardFields: Object,
            headers: Array,
        },
        data: () => ({
            dialog: false,
            dialogDelete: false,
            editedIndex: -1,
            editedItem: {},
            defaultItem: {},
            entries: [],
            successMessage: '',
            errorMessage: '',
            isDataLoading: false,
            selected: [],
        }),
        computed: {
            formTitle() {
                return this.editedIndex === -1 ? 'New Item' : 'Edit Item'
            },
        },
        watch: {
            dialog(val) {
                val || this.close()
            },
            dialogDelete(val) {
                val || this.closeDelete()
            },
        },
        mounted() {
            this.loadData();
        },
        methods: {
            handleSuccess(text) {
                this.successMessage = text;
                this.errorMessage = '';

                setTimeout(() => {
                    this.successMessage = '';
                }, 5000);
            },

            handleError(text) {
                this.errorMessage = text;
                this.successMessage = '';

                setTimeout(() => {
                    this.errorMessage = '';
                }, 5000);
            },

            editItem(item) {
                this.editedIndex = this.entries.indexOf(item)
                this.editedItem = Object.assign({}, item)
                this.dialog = true
            },

            deleteSelected() {
                this.dialogDelete = true
            },

            close() {
                this.dialog = false
                this.$nextTick(() => {
                    this.editedItem = Object.assign({}, this.defaultItem)
                    this.editedIndex = -1
                })
            },

            closeDelete() {
                this.dialogDelete = false
            },

            async loadData() {
                this.isDataLoading = true;
                try {
                    const response = await apiClient.get(this.selectedTableName);
                    this.entries = response.data;
                } catch (e) {
                    this.handleError(`Error loading data:${e}`);
                }
                this.isDataLoading = false;
            },

            async deleteSelectedConfirm() {
                for (let key in this.selected) {
                    let item = this.selected[key]
                    try {
                        await apiClient.delete(`${this.selectedTableName}/${item}${this.selectedTableName !== 'Projects' ? '?cascade=true' : ''}`);
                        let index = this.entries.findIndex(entry => entry.id === item)
                        this.entries.splice(index, 1)
                    } catch (e) {
                        this.handleError(`Error deleting item ${item}:${e}`);
                    }
                }
                this.closeDelete()
                this.selected = []
                this.handleSuccess('Successfully deleted data!')
            },

            async save() {
                try {
                    if (this.editedIndex > -1) {
                        const response = await apiClient.put(this.selectedTableName, this.editedItem);
                        Object.assign(this.entries[this.editedIndex], response.data)
                    } else {
                        await apiClient.post(this.selectedTableName, this.editedItem);
                        this.loadData();
                    }
                    this.handleSuccess('Succesfully saved data!')
                } catch (e) {
                    this.handleError(`Error updating data:${e}`);
                }
                this.close()
            },

            async switchTable(tableName) {
                this.$router.push(`/${tableName}`)
            }
        },
    };
</script>