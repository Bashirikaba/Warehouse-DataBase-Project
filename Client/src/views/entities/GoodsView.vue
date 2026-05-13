<script setup lang="ts">
import { goodsConfig } from '@/consts/tableConfigs'
</script>

<template>
  <Card>
    <template #content>
      <div class="entity-title">
        <h1>Товары</h1>
        <Button label="На домашнюю" as="router-link" :to="{ name: 'Home' }" />
      </div>
      <Divider></Divider>
      <EntityTable api-service="Goods" :config="goodsConfig"></EntityTable>
    </template>
  </Card>
</template>
