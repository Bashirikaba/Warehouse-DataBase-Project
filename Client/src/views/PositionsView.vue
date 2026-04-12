<script setup lang="ts">
import { onMounted } from 'vue'
import { usePositionsOperations } from '../viewModels/PositionsViewModel'

const { positions, getAllPositions } = usePositionsOperations()

onMounted(() => {
  getAllPositions()
})
// await Api.getEntity('Positions')
</script>
<template>
  <Card>
    <template #content>
      <div class="entity-title">
        <h1>Должности</h1>
        <Button label="На домашнюю" as="router-link" :to="{ name: 'Home' }" />
      </div>
      <DataTable :value="positions">
        <Column field="Name" header="Должность"></Column>
      </DataTable>
    </template>
  </Card>
</template>
